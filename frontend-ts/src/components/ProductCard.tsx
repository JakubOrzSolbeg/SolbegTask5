import React from "react";
import {FunctionComponent} from "react";
import ProductOverview from "../models/ProductOverview";

export const ProductCard: FunctionComponent<ProductOverview> =
    ({productId, productName, photoUrl, price}) =>{

        let formattedCurrency = price.toLocaleString('pl-PL', {
            minimumIntegerDigits: 1,
            useGrouping: false,
            maximumFractionDigits: 2,
            minimumFractionDigits: 2,
            currency: "pln"
        })

    return(
        <div className={"productCard"} onClick={() => {console.log("Clicked product "+productId)}}>
            <img src={require("../img/placeholder-image.png")} alt={productName} />
            <p>{productName} {formattedCurrency} PLN</p>
            <button>Add to cart</button>
        </div>
    )
}

