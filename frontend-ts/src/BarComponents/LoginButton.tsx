import React, {useRef} from "react";
import LoginForm from "../components/LoginForm";
import Popup from "reactjs-popup";

type LoginButtonProps = {
    onSuccesfulLogin: (token: string) => void
}


export default function LoginButton(props: LoginButtonProps){
    const ref = useRef(null);

    let button: any = <button>Login</button>
    const handleLogin= (token: string) =>{
        // @ts-ignore
        ref.current.close();
        props.onSuccesfulLogin(token);
        }

        return(
                <Popup ref={ref} open={false} trigger={button} modal>
                    <div className={"modal"}>
                        <LoginForm onLogin={handleLogin}/>
                    </div>
                </Popup>
        )
}