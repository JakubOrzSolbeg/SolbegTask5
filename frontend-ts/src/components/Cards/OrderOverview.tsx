import React from "react";
import OrderOverview from "../../models/OrderOverview";

export default function OrderOverviewCart(props: OrderOverview){
    let stateName: { [key: number]: string} = {
        0: "Pending",
        1: "Accepted",
        2: "Rejected"
    };

    return(
        <div className={"orderOverview "+stateName[props.status]}>
            <ul>
                <li> Order id: {props.orderId} </li>
                <li> Order date: { new Date(props.orderDate).toLocaleDateString() } </li>
                <li> Status: { stateName[props.status] }</li>
            </ul>

        </div>
    )

}