export default interface ShopAppState {
    firstRender: boolean,
    isLogged: boolean,
    authToken: string,
    cardItemCount: number,
    productsInCart: Array<number>,
}