import React from "react";
import GetOrderList from "../apiRequests/getOrderList";
import OrderOverview from "../models/OrderOverview";
import OrderOverviewCart from "./OrderOverview";

export default function OrderList(){
    let orderList: Array<OrderOverview> | undefined;
    let loadedSuccesfully: boolean = false;
    GetOrderList().then(result => {
        loadedSuccesfully = result.isSuccess
        if (result.body !== undefined) {
            return (
                <div className={"orders"}>
                    {result.body.map(order => {
                            return (
                                <OrderOverviewCart
                                    key={order.orderId}
                                    orderDate={order.orderDate}
                                    orderId={order.orderId}
                                    status={order.status}
                                />
                            )
                        }
                    )
                    }
                </div>
            )
        }
    } ).catch(err => {
        loadedSuccesfully = false
    })

    return (
        <div className={"orders"}>
            Is not loaded
        </div>
    )

}