import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";
import ProductDetails from "../models/ProductDetails";


export default function GetProductDetails(productId: number): Promise<ApiResult<ProductDetails>> {
    return fetch(apiBaseString+"/Shop/Products/"+productId, {
        method: "GET"
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<ProductDetails>
        })
}