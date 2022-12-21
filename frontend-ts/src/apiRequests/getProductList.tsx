import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";
import ProductOverview from "../models/ProductOverview";


export default function GetProductList(): Promise<ApiResult<Array<ProductOverview>>> {
    return fetch(apiBaseString+"/Shop/Products", {
        method: "GET"
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<Array<ProductOverview>>
        })
}