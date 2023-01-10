import ApiResult from "../models/ApiResultBase";
import ShopAppState from "../models/AppState";
import {apiBaseString} from "./requestsConfig";
import PendingOrderOverviewType from "../models/PendingOrderOverview";

export default function GetPendingOrders() : Promise<ApiResult<Array<PendingOrderOverviewType>>>{
    let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
    let token: string = memoryShopState.authToken;
    return fetch(apiBaseString+"/Orders/GetPendingOrders", {
        method: "GET",
        headers: {
            'Authorization': 'Bearer ' + token
        }
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<Array<PendingOrderOverviewType>>
        })
}