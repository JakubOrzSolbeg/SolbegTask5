import React, {useRef} from "react";
import ProductOverview from "../models/ProductOverview";
import Popup from "reactjs-popup";
import {ProductDetailedCard} from "./ProductDetailedCard";
import ConvertCurrency from "../utils/convertCurrency";

type ProductCardTypes = {
    productAlreadyInCart: boolean,
    product: ProductOverview,
    onProductMoveToCart: (productId: number) => void
}

export default function ProductCard(props: ProductCardTypes){
    const product = props.product
    const [src, setSrc] = React.useState(product.photoUrl);
    const formattedCurrency: string = ConvertCurrency(product.price);

    const ref = useRef(null);

    const onAddedToCart = () =>{
        // @ts-ignore
        ref.current.close();
        props.onProductMoveToCart(product.productId);
    }

    const imgPlaceholder = require("../img/placeholder-image.png")
    const productCart = <div className={"productCard"}>

                            <img
                                placeholder={imgPlaceholder}
                                src={src}
                                onError={() => setSrc(imgPlaceholder)}
                                alt={product.productName} />
                            <p>{product.productName } {formattedCurrency} PLN</p>
                        </div>

    return(
        <div>
            <Popup ref={ref} open={false} trigger={productCart} modal>
                    <ProductDetailedCard
                        onMoveToCart={onAddedToCart}
                        isAlreadyInCart={props.productAlreadyInCart}
                        productId={product.productId}/>
            </Popup>
        </div>

    )
}

