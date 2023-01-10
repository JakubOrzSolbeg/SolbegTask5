import React, {useRef} from "react";
import {CheckoutResult} from "../components/CheckoutResult";
import LoginForm from "../components/LoginForm";
import Popup from "reactjs-popup";

type CheckoutButtonProps = {
    isLoggedIn: boolean
    onLogin: (token: string) => void
    onCartClear: () => void
}

export default function CheckoutButton(props: CheckoutButtonProps){

    const handleLogin= (token: string) =>{
        // @ts-ignore
        ref.current.close();
        props.onLogin(token);
    }

    const popUpResult = (props.isLoggedIn)? <CheckoutResult onCartClear={props.onCartClear} /> : <LoginForm onLogin={handleLogin} />
    const ref = useRef(null);

    return(
        <Popup ref={ref} trigger={<button> Checkout </button>} modal>
            <div className={"modal"}>
                {popUpResult}
            </div>
        </Popup>
    )

}