import React from "react";
import OrderOverview from "../models/OrderOverview";
import Loader from "../components/Shared/Loader";
import GetOrderList from "../apiRequests/getOrderList";
import OrderOverviewCart from "../components/Cards/OrderOverview";

type OrderListState = {
    isLoaded: boolean
    isSuccessful: boolean
    data: Array<OrderOverview>
}


export class OrderList2 extends React.Component<any, OrderListState> {
    state: OrderListState = {
        isLoaded: false,
        isSuccessful: true,
        data: []
    }

    componentDidMount() {
        GetOrderList().then(res =>{
            this.setState({
                isLoaded: true,
                isSuccessful: res.isSuccess,
                data: res.body??[]
            })
        })
    }

    render() {
        if (!this.state.isLoaded){
            return (<Loader />)
        }

        if (this.state.isSuccessful){
            return (
                <div className={"order-list"}>
                    <h3> Your order history </h3>
                    {this.state.data.map(order => {
                        return(
                            <OrderOverviewCart
                                key={order.orderId}
                                orderId={order.orderId}
                                status={order.status}
                                orderDate={order.orderDate}

                            />
                        )
                    })}
                </div>
            )
        }
        else{
            return (<p> Could not download order list</p>)
        }
    }
}