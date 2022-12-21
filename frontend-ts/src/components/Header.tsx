import React from "react";
import LoginButton from "../BarComponents/LoginButton";


export default function Header(){
    return(
        <div className={"NavBar"}>
            <div>
                <LoginButton />
            </div>
            <div>
                <button>
                    Shopping Cart
                </button>
            </div>

            <div className={"logo"}>
                <h1>
                    Jakubo Shop
                </h1>
            </div>
        </div>
    )
}