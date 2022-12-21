import React from "react";
import Popup from "reactjs-popup";
import GetProductList from "../apiRequests/getProductList";
import {ProductCard} from "./ProductCard";

export default function MainComponent(){

    GetProductList().then(result => {
        return (
            <div>
                <Popup trigger={<button> Trigger</button>} position="center center" modal nested>
                    <div>Popup content here !!</div>
                </Popup>
            </div>
        )
    });
}