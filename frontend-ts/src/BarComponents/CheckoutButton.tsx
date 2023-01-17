import React, {useRef} from "react";
import {CheckoutResult} from "../components/Cards/CheckoutResult";
import Popup from "reactjs-popup";
import {LoginCard} from "../components/Cards/LoginCard";

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

    const popUpResult = (props.isLoggedIn)? <CheckoutResult onCartClear={props.onCartClear} /> : <LoginCard onLogin={handleLogin} />
    const ref = useRef(null);

    return(
        <Popup ref={ref} trigger={<button> Checkout </button>} modal>
            <div className={"modal"}>
                {popUpResult}
            </div>
        </Popup>
    )

}