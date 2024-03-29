import React from "react";
import ProductOverview from "../models/ProductOverview";
import GetProductList from "../apiRequests/getProductList";
import Loader from "../components/Shared/Loader";
import ProductCard from "../components/Cards/ProductCard";
import ShopAppState from "../models/AppState";
import {PaginationController} from "../components/Forms/paginationController";
import {SearchForm, SearchFormParams} from "../components/Forms/SearchForm";
import ApiResult from "../models/ApiResultBase";
import MakeApiCall from "../apiRequests/MainApiRequester";

type ShopProps = {
    appState: ShopAppState,
    onProductAdded: (product: number) => void
    onProductRemoved: (product: number) => void
}

type ShopState = {
    products: Array<ProductOverview>;
    isLoaded: boolean;
    loadError: string;
    currentPage: number;
    itemsPerPage: number;
    maxPage: number;
};

class Shop extends React.Component<ShopProps, ShopState> {
    constructor(props: ShopProps) {
        super(props);
        this.changePage = this.changePage.bind(this);
        this.changeItemsPerPage = this.changeItemsPerPage.bind(this);
        this.searchProducts = this.searchProducts.bind(this);
    }

    state: ShopState = {
        products: [],
        isLoaded: false,
        loadError: "",
        currentPage: 1,
        maxPage: 1,
        itemsPerPage: 5
    };

    changePage(page: number): void{
        this.setState({currentPage: Math.max(1, page)})
    }

    changeItemsPerPage(items: number): void{
        let newItemsPerPage: number = Math.max(1, items);
        let itemCount = this.state.products.length
        let newMaxPage: number = Math.ceil(itemCount / newItemsPerPage)
        this.setState({
            itemsPerPage: newItemsPerPage,
            maxPage: newMaxPage,
            currentPage: Math.min(this.state.currentPage, newMaxPage)
        })
    }

    searchProducts(searchParams: SearchFormParams){
        MakeApiCall<SearchFormParams, ProductOverview[]>(
            "/Shop/Products", 'POST', false, searchParams
        ).then(result => {
            this.loadData(result)
        })

    }

    loadData(result: ApiResult<ProductOverview[]>){
        if (!result.isSuccess){
            this.setState({isLoaded : true, loadError: result.errors})
        }
        else{
                this.setState({
                isLoaded: true,
                products: result.body??[],
                maxPage: Math.ceil(((result.body?.length??1) / this.state.itemsPerPage))
            })
        }
    }

    componentDidMount() {
        GetProductList().then(result => this.loadData(result));
    }

    render() {

        if(!this.state.isLoaded){
            return (<Loader />);
        }

        if(this.state.loadError !== ""){
            return (<div className={"error-message"}> Something went wrong :(</div>)
        }

        return (
            <div className={"shop-main-page"}>
                <SearchForm onSearch={this.searchProducts}/>
                <PaginationController
                    changeItemPerPage={this.changeItemsPerPage}
                    maxPage={this.state.maxPage}
                    changePage={this.changePage}
                    currentPage={this.state.currentPage}
                    subclass={"upper"}
                />
                <div className={"products"}>
                    {this.state.products.slice(
                        (this.state.currentPage - 1) * this.state.itemsPerPage,
                        (this.state.currentPage * this.state.itemsPerPage)
                    ).map(product => {
                        const productAlreadyInCart = this.props.appState.productsInCart.includes(product.productId);
                        return (
                            <ProductCard productAlreadyInCart={productAlreadyInCart}
                                         product={product}
                                         onProductMoveToCart={(productAlreadyInCart)? this.props.onProductRemoved : this.props.onProductAdded}
                                         key={product.productId} />
                        )
                    })}
                </div>
                <PaginationController
                    changeItemPerPage={this.changeItemsPerPage}
                    maxPage={this.state.maxPage}
                    changePage={this.changePage}
                    currentPage={this.state.currentPage}
                    subclass={"bottom"}
                />
            </div>
        );
    }
}

export default Shop