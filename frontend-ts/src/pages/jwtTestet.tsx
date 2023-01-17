import React from "react";
import ShopAppState from "../models/AppState";
import GetOrderList from "../apiRequests/getOrderList";


export default function JwtTester(){
        let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
        if (memoryShopState.authToken === ""){
            console.log("Pusty token")
        }
        else{
            GetOrderList().then(res => {
                console.log(res)
            })
        }
        return(
            <div>
                Tutaj coś jest jednak
            </div>
        )

}