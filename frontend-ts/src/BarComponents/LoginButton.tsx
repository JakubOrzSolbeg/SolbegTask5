import React, {useRef} from "react";
import LoginForm from "../components/LoginForm";
import Popup from "reactjs-popup";

export default function LoginButton(){
    const ref = useRef(null);

    let button: any = <button>Login</button>
    const handleClose = () =>{
        console.log("Handle close")
        // @ts-ignore
        ref.current.close();
        }

        return(
            <div className={"bar-button"}>
                <Popup ref={ref} open={false} trigger={button} modal>
                    <div className={"modal"}>
                        <LoginForm onclose={handleClose}/>
                    </div>
                </Popup>
            </div>
        )
}