import React from "react";
import MakeOrder from "../../apiRequests/makeOrder";
import Loader from "../Shared/Loader";

type CheckoutResultProps = {
    onCartClear: () => void
}

type CheckoutResultState = {
    isCompleted: boolean,
    isSuccessful: boolean
}

export class CheckoutResult extends React.Component<CheckoutResultProps, CheckoutResultState>{
    constructor(props: CheckoutResultProps) {
        super(props);
        this.returnToMenu = this.returnToMenu.bind(this)
        this.returnToCart = this.returnToCart.bind(this);
    }

    state: CheckoutResultState = {
        isCompleted: false,
        isSuccessful: false
    }

    returnToMenu(){
        this.props.onCartClear();
        window.location.replace("/");
    }

    returnToCart(){
        window.location.replace("/myCart");
    }

    componentDidMount() {
        MakeOrder().then(result => {
            this.setState({
                isCompleted: true,
                isSuccessful: result.isSuccess
            })
        })
    }

    render(){
        if (!this.state.isCompleted){
            return (
                <div className={"checkoutResult"}>
                    <Loader />
                </div>
            )
        }
        if (this.state.isSuccessful) {
            return (
                <div className={"checkoutResult"}>
                    <img src={require("../../img/success.gif")} alt={"succes"}/>
                    <h3> Payment successful </h3>
                    <button onClick={this.returnToMenu}> Return to shop</button>
                </div>
            )
        }
        else{
            return (
                <div className={"checkoutResult"}>
                    <img src={require("../../img/fail.png")} alt={"fail"}/>
                    <h3> Payment payment failed </h3>
                    <button onClick={this.returnToCart}> Return to cart </button>
                </div>
            )
        }
    }

}