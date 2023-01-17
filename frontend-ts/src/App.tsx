import React from 'react';
import Header from "./components/Shared/Header";
import Footer from "./components/Shared/Footer";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import Shop from "./pages/Shop";
import ShopAppState from "./models/AppState";
import {ShopCardList} from "./components/Lists/ShopCardList";
import JwtTester from "./pages/jwtTestet";
import {OrderList2} from "./pages/OrderList2";
import {WorkerPanel} from "./pages/WorkerPanel";

export class App extends React.Component<any, ShopAppState> {
    state: ShopAppState= {
        firstRender: true,
        isLogged: false,
        productsInCart: [],
        cardItemCount: 0,
        authToken: ""
    }

    constructor(props: any) {
        super(props);
        this.onSuccesfullLogin = this.onSuccesfullLogin.bind(this);
        this.onLogout = this.onLogout.bind(this);
        this.onProductAdded = this.onProductAdded.bind(this);
        this.onProductRemoved = this.onProductRemoved.bind(this)
        this.onClearCart = this.onClearCart.bind(this);
    }

    componentDidUpdate(prevProps: Readonly<any>, prevState: Readonly<ShopAppState>, snapshot?: any) {
        localStorage.setItem("shopState", JSON.stringify(this.state))
    }


    componentDidMount() {
        if (this.state.firstRender){
            let memoryShopState = JSON.parse(localStorage.getItem("shopState")??"{}") as ShopAppState;
            memoryShopState.firstRender = false;
            this.setState(memoryShopState);
        }
    }

    onProductAdded(productId: number){
        if (this.state.productsInCart)
        this.setState({
            cardItemCount: this.state.cardItemCount + 1,
            productsInCart: [...this.state.productsInCart, productId]
        })
    }

    onProductRemoved(productId: number){
        if (this.state.productsInCart.includes(productId)){
            this.setState({
                cardItemCount: this.state.cardItemCount - 1,
                productsInCart: this.state.productsInCart.filter(product => product !== productId)
            })
        }
    }

    onClearCart(){
        this.setState({
            cardItemCount: 0,
            productsInCart: []
        })
        window.location.replace("/")
    }

    onSuccesfullLogin(token: string){
        this.setState({isLogged: true, authToken: token})
    }

    onLogout(){
        this.setState({isLogged: false, authToken: ""})
        window.location.replace("/");
    }

    render(){
          return(
            <div className="App">
                <Header
                    appState={this.state}
                    onProductAdd={this.onProductAdded}
                    onSuccesfulLogin={this.onSuccesfullLogin}
                    onLogout={this.onLogout}
                />
                <main>
                  <Router>
                      <Routes>
                          <Route path={"/"} element={
                              <Shop onProductRemoved={this.onProductRemoved}
                                    appState={this.state}
                                    onProductAdded={this.onProductAdded}
                              />} />
                          <Route path={"/myCart"} element={
                              <ShopCardList
                                  onProductRemove={this.onProductRemoved}
                                  onLogin={this.onSuccesfullLogin}
                                  shopAppState={this.state}
                                  onClearCart={this.onClearCart}
                                />
                          } />
                          <Route path={"/token"} element={
                              <JwtTester />
                          } />
                          <Route path={"orders"} element={
                              <OrderList2 />
                          } />
                          <Route path={"workerPanel"} element={
                              <WorkerPanel />
                          } />
                      </Routes>
                  </Router>
                </main>
                <Footer />
            </div>
        )
  }
}

