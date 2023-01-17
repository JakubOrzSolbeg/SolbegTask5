import React from "react";
import MakeApiCall from "../../apiRequests/MainApiRequester";

type ProductFormType = {
    productName: string
    productDetail: string
    price: number
    photoUrl: string
    brand: string
    category: string
}


export class ProductForm extends React.Component<any, ProductFormType>{
    constructor(props: any) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    state: ProductFormType = {
        productName: "",
        price: 20,
        productDetail: "No details provided",
        brand: "",
        category: "",
        photoUrl: ""
    }

    handleChange(evt: React.ChangeEvent<HTMLInputElement>){
        const value = evt.target.value;
        this.setState({
            ...(this.state),
            [evt.target.name]: value
        });
    }

    handleSubmit(event: any){
        console.log(this.state)
        MakeApiCall<ProductFormType, boolean>("/Shop/AddProduct", "POST", true, this.state)
            .then(result => console.log(result))
        event.preventDefault();
    }

    render() {
        return (
            <form className={"product-form"} onSubmit={this.handleSubmit}>

                <label>
                    Product name:
                    <input type={"text"} name={"productName"} value={this.state.productName} required={true} onChange={this.handleChange}/>
                </label>
                <label>
                    Prize:
                    <input type={"number"} min={0} name={"price"} value={this.state.price} required={true} onChange={this.handleChange}/>
                </label>
                <label>
                    Category:
                    <input type={"text"} name={"category"} value={this.state.category} required={true} onChange={this.handleChange}/>
                </label>
                <label>
                    Brand:
                    <input type={"text"} name={"brand"} value={this.state.brand} required={true} onChange={this.handleChange}/>
                </label>
                <label>
                    Description:
                    <input type={"text"} name={"productDetail"} value={this.state.productDetail} required={true} onChange={this.handleChange}/>
                </label>
                <label>
                    Photo url:
                    <input type={"text"} name={"photoUrl"} value={this.state.photoUrl} required={true} onChange={this.handleChange}/>
                </label>
                <button type={"submit"} onSubmit={this.handleSubmit}> Add product </button>
            </form>
        )
    }

}