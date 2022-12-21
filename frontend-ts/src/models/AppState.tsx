import React from "react";

export default interface ShopAppState {
    isLogged: boolean,
    authToken: string,
    cardItemCount: number,
    productsInCart: Array<number>
}