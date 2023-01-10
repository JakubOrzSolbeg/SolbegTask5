import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";
import OrderOverview from "../models/OrderOverview";
import ShopAppState from "../models/AppState";


export default function GetOrderList() : Promise<ApiResult<Array<OrderOverview>>>{
    let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
    let token: string = memoryShopState.authToken;
    return fetch(apiBaseString+"/Orders/GetOrders", {
        method: "GET",
        headers: {
            'Authorization': 'Bearer ' + token
        }
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<Array<OrderOverview>>
        })
}