import React from "react";

type PendingOrderOverviewProps = {
    onDecision: (id: number, accepted: boolean) => void
    orderId: number,
    userId: number,
    products: Array<string>
}

export default function PendingOrderOverview(props: PendingOrderOverviewProps){
    return(
        <div className={"pending-order"}>
            <ul>
                <li> OrderId: {props.orderId} </li>
                <li> BuyerId: {props.userId}</li>
                <li> Products:
                    <ol>
                        {props.products.map(product => {
                            return (<li key={product}> {product}</li>);
                        })}
                    </ol>
                </li>
            </ul>
            <button onClick={() => props.onDecision(props.orderId,true)} className={"accept"}>Accept</button>
            <button onClick={() => props.onDecision(props.orderId, false)} className={"reject"}>Reject</button>
        </div>
    )
}