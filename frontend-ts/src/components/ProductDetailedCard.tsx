import React from "react";
import GetProductDetails from "../apiRequests/getProductDetails";
import ProductDetails from "../models/ProductDetails";
import Loader from "./Loader";
import ConvertCurrency from "../utils/convertCurrency";

type ProductDetailedCardProps = {
    productId: number
    onMoveToCart: () => void
    isAlreadyInCart: boolean
}

type ProductDetailedCardState = {
    isLoaded: boolean,
    loadedSuccessfully: boolean,
    errors: string,
    data?: ProductDetails
}

export class ProductDetailedCard extends React.Component<ProductDetailedCardProps, ProductDetailedCardState>{

    constructor(props: ProductDetailedCardProps) {
        super(props);
        this.onPhotoError = this.onPhotoError.bind(this);
    }

    state: ProductDetailedCardState = {
        loadedSuccessfully: true,
        isLoaded: false,
        errors: "",
        data: undefined
    }

    componentDidMount() {
        GetProductDetails(this.props.productId).then(result => {
            this.setState({
                data : result.body,
                isLoaded : true,
                errors : result.errors,
                loadedSuccessfully : result.isSuccess}
            )
        })
    }

    onPhotoError() {
        const someProperty: ProductDetails = this.state.data!;
        someProperty.photoUrl = require("../img/placeholder-image.png")
        this.setState({data: someProperty})
    }

    render(){

        const buttonText: string = (this.props.isAlreadyInCart)? "Remove from cart" : "Add do cart";

        if (!this.state.isLoaded){
            return (<Loader />)
        }

        if (!this.state.loadedSuccessfully){
            return (<p> Something went terribly wrong <br /> {this.state.errors} </p>)
        }
        const formattedCurrency: string = ConvertCurrency(this.state.data?.price??0);
        return (
        <div className={"productCard productCardDetailed"}>
            <img
                src={this.state.data?.photoUrl}
                onError={this.onPhotoError}
                alt={this.state.data?.productName} />
            <p>{this.state.data?.productName} {formattedCurrency} PLN</p>
            <p> {this.state.data?.productDetail}</p>
            <p> {this.state.data?.brand}</p>
            <button onClick={this.props.onMoveToCart}> {buttonText} </button>
        </div>
        )}
}