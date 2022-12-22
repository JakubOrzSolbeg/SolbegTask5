import React from "react";

type CheckoutResultProps = {
    onCartClear: () => void
}

export default function CheckoutResult(props: CheckoutResultProps){
    const returnToMenu = () =>{
        props.onCartClear();
        window.location.replace("/")
    }

    return(
        <div className={"checkoutResult"}>
            <img src={require("../img/success.gif")} alt={"succes"} />
            <h3> Payment successful </h3>
            <button onClick={returnToMenu}> Return to shop </button>
        </div>
    )
}