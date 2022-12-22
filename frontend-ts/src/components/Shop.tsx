import React from "react";
import ProductOverview from "../models/ProductOverview";
import GetProductList from "../apiRequests/getProductList";
import Loader from "./Loader";
import ProductCard from "./ProductCard";
import ShopAppState from "../models/AppState";

type ShopProps = {
    appState: ShopAppState,
    onProductAdded: (product: number) => void
    onProductRemoved: (product: number) => void
}

type ShopState = {
    products: Array<ProductOverview>;
    isLoaded: boolean;
    loadError: string;
};

class Shop extends React.Component<ShopProps, ShopState> {

    state: ShopState = {
        products: [],
        isLoaded: false,
        loadError: ""
    };

    componentDidMount() {
        GetProductList().then(result => {
            console.log(result);
          if (!result.isSuccess){
              this.setState({isLoaded : true, loadError: result.errors})
          }
          else{
              this.setState({isLoaded: true, products: result.body??[]})
          }
        });
    }

    render() {

        if(!this.state.isLoaded){
            return (<Loader />);
        }

        if(this.state.loadError !== ""){
            return (<div className={"error-message"}> Something went wrong :(</div>)
        }

        return (
            <div className={"products"}>
                {this.state.products.map(product => {
                    const productAlreadyInCart = this.props.appState.productsInCart.includes(product.productId);
                    return (
                        <ProductCard productAlreadyInCart={productAlreadyInCart}
                                     product={product}
                                     onProductMoveToCart={(productAlreadyInCart)? this.props.onProductRemoved : this.props.onProductAdded}
                                     key={product.productId} />
                    )
                })}
            </div>
        );
    }
}

export default Shop