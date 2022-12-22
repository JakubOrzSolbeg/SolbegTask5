import React from "react";
import LoginButton from "../BarComponents/LoginButton";
import ShopAppState from "../models/AppState";
import CartButton from "../BarComponents/CartButton";

type HeaderProps = {
    appState: ShopAppState,
    onProductAdd: (productId: number) => void,
    onSuccesfulLogin: (token: string) => void,
    onLogout: () => void
}


export default function Header(props: HeaderProps){

    let loginButton: JSX.Element;
    if (props.appState.isLogged){
        loginButton = <button onClick={props.onLogout}> Logout </button>
    }
    else{
        loginButton = <LoginButton onSuccesfulLogin={props.onSuccesfulLogin} />
    }

    return(
        <div className={"NavBar"}>
            <div className={"barButton"}>
                {loginButton}
            </div>
            <CartButton shopAppState={props.appState} />

            <div className={"logo"}>
                <h1 onClick={() => window.location.replace("/") }>
                    Jakubo Shop
                </h1>
            </div>
        </div>
    )
}