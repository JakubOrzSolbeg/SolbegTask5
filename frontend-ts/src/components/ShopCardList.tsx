import React from "react";
import ShopAppState from "../models/AppState";
import CheckoutButton from "../BarComponents/CheckoutButton";
import ProductOverview from "../models/ProductOverview";
import GetProductsInCart from "../apiRequests/getProductsInCart";
import Loader from "./Loader";
import ProductCard from "./ProductCard";

type ShopCardListProps = {
    shopAppState: ShopAppState
    onLogin: (token: string) => void
    onClearCart: () => void
    onProductRemove: (productId: number) => void
}

type ShopCardState = {
    isLoaded: boolean,
    loadedSuccefully: boolean,
    errors: string,
    data?: Array<ProductOverview>,
    productsCount: number
}

export class ShopCardList extends React.Component<ShopCardListProps, ShopCardState>{

    constructor(props: ShopCardListProps) {
        super(props);
        this.onProductRemove = this.onProductRemove.bind(this);
    }

    state: ShopCardState = {
        isLoaded: false,
        errors: "",
        loadedSuccefully: true,
        data : [],
        productsCount: 0
    }

    componentDidUpdate(prevProps: Readonly<ShopCardListProps>, prevState: Readonly<ShopCardState>, snapshot?: any) {
        if (this.props.shopAppState.cardItemCount <= 0){
            window.location.replace("/");
        }

        if (this.state.productsCount !== this.props.shopAppState.cardItemCount && this.props.shopAppState.cardItemCount > 0) {
            this.loadData();
        }
    }

    loadData() {
        GetProductsInCart(this.props.shopAppState.productsInCart).then(res => {
            this.setState({
                isLoaded: true,
                errors: res.errors,
                loadedSuccefully: res.isSuccess,
                data: res.body,
                productsCount: this.props.shopAppState.cardItemCount
            })
        })
    }

    componentDidMount() {
        if (this.state.productsCount > 0) {
            this.loadData();
        }
        else{
            this.setState({isLoaded: true, loadedSuccefully: true})
        }
    }

    onProductRemove(productId: number){
        this.props.onProductRemove(productId);
    }

    render() {
        if (!this.state.isLoaded){
            return (<Loader />)
        }

        if (!this.state.loadedSuccefully){
            return (<div> {this.state.errors} </div>)
        }

        return (
            <div>
                <div className={"products"}>
                    {this.state.data?.map(product => {
                        return(<ProductCard
                                key={product.productId}
                                productAlreadyInCart={true}
                                product={product}
                                onProductMoveToCart={this.onProductRemove}
                            />
                        )}
                    )
                    }
                </div>
                <div className={"decision-buttons"}>
                    <CheckoutButton
                        isLoggedIn={this.props.shopAppState.isLogged}
                        onLogin={this.props.onLogin}
                        onCartClear={this.props.onClearCart}
                    />
                    <button onClick={this.props.onClearCart}>
                        Clear shopping cart
                    </button>
                </div>
            </div>

        );
    }

}