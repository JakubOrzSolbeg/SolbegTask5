import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";
import ShopAppState from "../models/AppState";

export default function MakeOrder(): Promise<ApiResult<boolean>> {
    let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
    let request = {
        productIds: memoryShopState.productsInCart
    }
    return fetch(apiBaseString+"/Orders/PlaceOrder", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json' ,
            'Authorization': 'Bearer ' + memoryShopState.authToken
        },
        body: JSON.stringify(request)
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<boolean>
        })
}