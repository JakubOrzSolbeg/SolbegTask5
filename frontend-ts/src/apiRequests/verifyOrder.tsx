import ApiResult from "../models/ApiResultBase";
import OrderOverview from "../models/OrderOverview";
import ShopAppState from "../models/AppState";
import {apiBaseString} from "./requestsConfig";

export default function VerifyOrder(accept: boolean, id: number) : Promise<ApiResult<boolean>>{
    let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
    let token: string = memoryShopState.authToken;
    let method = (accept ? "Accept" : "Reject");
    return fetch(apiBaseString+`/Orders/${method}Order?orderId=${id}`, {
        method: "POST",
        headers: {
            'Authorization': 'Bearer ' + token
        }
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<boolean>
        })
}