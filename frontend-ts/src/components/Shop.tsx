import React from "react";
import ProductOverview from "../models/ProductOverview";
import GetProductList from "../apiRequests/getProductList";
import Loader from "./Loader";
import {ProductCard} from "./ProductCard";


type ShopState = {
    products: Array<ProductOverview>;
    isLoaded: boolean;
    loadError: string;
};

class Shop extends React.Component<any, ShopState> {

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
                    return (
                        <ProductCard
                        photoUrl={product.photoUrl}
                        productId={product.productId}
                        productName={product.productName}
                        price={product.price}
                        key={product.productId}
                        />
                    )
                })}
            </div>
        );
    }
}

export default Shop