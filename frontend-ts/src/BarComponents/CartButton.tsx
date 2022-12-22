import React from "react";
import ShopAppState from "../models/AppState";


type CartButtonProps = {
    shopAppState: ShopAppState
}

export default function CartButton(props: CartButtonProps){
    const shopCartCount = (props.shopAppState.cardItemCount > 0)?
        <div className={"shopCartItemCount"}> {props.shopAppState.cardItemCount} </div> :
        <div> </div>

    return(
        <div className={"barButton"}>
            {shopCartCount}
            <button disabled={(props.shopAppState.cardItemCount < 1)} onClick={() => window.location.replace("/myCart")}> Shop cart</button>
        </div>
    )
}