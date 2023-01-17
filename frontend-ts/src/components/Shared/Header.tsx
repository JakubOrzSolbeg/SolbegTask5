import React from "react";
import LoginButton from "../../BarComponents/LoginButton";
import ShopAppState from "../../models/AppState";
import CartButton from "../../BarComponents/CartButton";
import jwt_decode from "jwt-decode";

type HeaderProps = {
    appState: ShopAppState,
    onProductAdd: (productId: number) => void,
    onSuccesfulLogin: (token: string) => void,
    onLogout: () => void
}


export default function Header(props: HeaderProps){

    let loginButton: JSX.Element;
    let logged_links;
    let worker_links;
    if (props.appState.isLogged){
        let token = jwt_decode<any>(props.appState.authToken);
        if (token["role"] === "Worker"){
            worker_links = <div className={"barButton"}>
                <button onClick={() => window.location.replace("/workerPanel")}>Worker panel</button>
            </div>
        }
        loginButton = <button onClick={props.onLogout}> Logout </button>
        logged_links = <div className={"barButton"}>
            <button onClick={() => window.location.replace("/orders")}>Orders</button>
        </div>
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
            {logged_links}
            {worker_links}
            <div className={"logo"}>
                <h1 onClick={() => window.location.replace("/") }>
                    Jakubo Shop
                </h1>
            </div>
        </div>
    )
}