import React from "react";
import GetPendingOrders from "../apiRequests/getPendingOrders";
import PendingOrderOverviewType from "../models/PendingOrderOverview";
import Loader from "../components/Shared/Loader";
import PendingOrderOverview from "../components/Cards/PendingOrderOverview";
import VerifyOrder from "../apiRequests/verifyOrder";
import {ProductForm} from "../components/Forms/ProductForm";

type WorkPanelState = {
    isLoaded: boolean
    isSucses: boolean
    pendingOrders: Array<PendingOrderOverviewType>
}


export class WorkerPanel extends React.Component<any, WorkPanelState>{
    constructor(props: any) {
        super(props);
        this.makeDecision = this.makeDecision.bind(this);
    }

    state: WorkPanelState = {
        isLoaded: false,
        isSucses: false,
        pendingOrders: []
    }

    makeDecision(id: number, accept: boolean){
        VerifyOrder(accept, id).then(res => {
            if (res.isSuccess){
                this.setState({pendingOrders: this.state.pendingOrders.filter(item => item.orderId !== id)});
            }
        })
    }

    componentDidMount() {
        GetPendingOrders().then(res =>{
            this.setState({
                isLoaded: true,
                isSucses: res.isSuccess,
                pendingOrders: res.body??[]
            })
        })
    }

    render() {
        if (!this.state.isLoaded){
            return (<Loader />)
        }
        if (!this.state.isSucses){
            return (<p> Error with loadingdata</p>)
        }
        else{
            return(
                <div className={"workerPanel"}>
                    <h1> Your worker panel</h1>
                    <h2> Pending orders</h2>
                    <div className={"pending-order-list"}>
                        {this.state.pendingOrders.map(order => {
                            return (
                                <PendingOrderOverview
                                    key={order.orderId}
                                    orderId={order.orderId}
                                    userId={order.userId}
                                    products={order.productNames}
                                    onDecision={this.makeDecision}
                                />
                            );
                        })}
                    </div>
                    <h2> Add new product </h2>
                    <ProductForm />
                </div>
            )
        }

    }
}