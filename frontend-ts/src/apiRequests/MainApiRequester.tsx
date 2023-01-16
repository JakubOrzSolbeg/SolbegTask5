import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";
import ShopAppState from "../models/AppState";

export default function MakeApiCall<TInput, TResult>(
    path: string = "/", method: string = "GET", needAuth: boolean = false, data: TInput )
    : Promise<ApiResult<TResult>>{
    const requestUrl = apiBaseString + path;
    let requestInit: RequestInit = {};
    requestInit.method = method;
    if (needAuth){
        let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
        requestInit.headers = {
            'Content-Type': 'application/json' ,
                'Authorization': 'Bearer ' + memoryShopState.authToken
        }
    }
    else{
        requestInit.headers = {
            'Content-Type': 'application/json'
        }
    }
    if(data !== null){
        requestInit.body = JSON.stringify(data);
    }

    return fetch(requestUrl, requestInit)
        .then(res => res.json())
        .then(res => res as ApiResult<TResult>)
}