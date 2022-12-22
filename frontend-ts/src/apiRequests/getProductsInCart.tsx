import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";
import ProductOverview from "../models/ProductOverview";


export default function GetProductsInCart(products: Array<number>): Promise<ApiResult<Array<ProductOverview>>> {
    return fetch(apiBaseString+"/Shop/ShopCart", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(products)
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<Array<ProductOverview>>
        })
}