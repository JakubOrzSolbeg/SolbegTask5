import React from "react";
import MakeApiCall from "../../apiRequests/MainApiRequester";
import ShopInfo from "../../models/ShopInfo";


type SearchFormProps = {
    onSearch: (args: SearchFormParams) => void
}


export type SearchFormParams = {
    minPrice: number | undefined
    maxPrice: number | undefined,
    name: string,
    categories: Array<number>
    brands: Array<number>
}

type SearchFormState = {
    searchParams: SearchFormParams,
    shopDetails?: ShopInfo
}


export class SearchForm extends React.Component<SearchFormProps, SearchFormState>{
    constructor(props: SearchFormProps) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    state: SearchFormState = {
        searchParams: {
            minPrice: undefined,
            maxPrice: undefined,
            name: "",
            categories: [],
            brands: []
        },
        shopDetails:{
            brands: [],
            categories: []
        }
    }

    componentDidMount() {

        MakeApiCall<null, ShopInfo>("/Shop/ShopDetails", "GET", false, null)
            .then(res => this.setState({shopDetails: res.body}));
    }


    handleChange(evt: React.ChangeEvent<HTMLInputElement>){
            switch (evt.target.name){
            case "minPrice":
                let newMinValue = evt.target.value === "" ? undefined : parseInt(evt.target.value)

                this.setState({searchParams: {...this.state.searchParams, minPrice: newMinValue } })
                break;
            case "maxPrice":
                let newMaxValue = evt.target.value === "" ? undefined : parseInt(evt.target.value)

                this.setState({searchParams: {...this.state.searchParams, maxPrice: newMaxValue } })
                break;
            case "brand":
                console.log("Brand");
                break;
            case "category":
                console.log("Category");
                break;
            default:
                console.log("unknown");
                break;
        }
    }

    handleSubmit(evt: React.FormEvent<HTMLFormElement>){
        this.props.onSearch(this.state.searchParams)
        evt.preventDefault()
    }

    render() {
        return (
            <div className={"search-box"}>
                <form onSubmit={this.handleSubmit}>
                    <label> Mix price: </label>
                    <input
                        type={"number"}
                        min={0}
                        name={"minPrice"}
                        value={this.state.searchParams.minPrice === undefined ? '' : this.state.searchParams.minPrice.toString()}
                        onChange={this.handleChange}
                    />
                    <label> Max price: </label>
                    <input
                        type={"number"}
                        defaultValue={undefined}
                        min={0}
                        name={"maxPrice"}
                        value={this.state.searchParams.maxPrice === undefined ? '' : this.state.searchParams.maxPrice.toString()}
                        onChange={this.handleChange}
                    />
                    <label> Category: </label>
                    {this.state.shopDetails?.categories.map(category => {
                        return (
                            <label key={category.categoryId}>
                                {category.categoryName}
                            <input
                                onChange={this.handleChange}
                                type={"checkbox"}
                                id={`category-${category.categoryId}`}
                                name={'category'}
                                value={category.categoryName}/>
                            </label>)
                    })}
                    <label> Brand: </label>
                    {this.state.shopDetails?.brands.map(brand => {
                        return (
                            <label key={brand.brandId}>
                                {brand.brandName}
                                <input
                                    onChange={this.handleChange}
                                    type={"checkbox"}
                                    id={`brand-${brand.brandId}`}
                                    name={'brand'}
                                    value={brand.brandName}/>
                            </label>)
                    })}

                    <input type={"submit"} value={"Search"}/>
                </form>
            </div>
        )
    }
}