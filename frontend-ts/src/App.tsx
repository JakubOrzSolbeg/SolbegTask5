import React from 'react';
import Header from "./components/Header";
import Footer from "./components/Footer";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import SecondComponent from "./components/SecondComponent";
import Shop from "./components/Shop";


import ControlledPopup from "./components/testPopUp";
import ShopAppState from "./models/AppState";

export class App extends React.Component<any, ShopAppState> {
    constructor(props: any) {
        super(props);
        this.setState((JSON.parse(localStorage.getItem("shopState")??"{}")) as ShopAppState)
        console.log(this.state);
    }
    state: ShopAppState= {
        isLogged: false,
        productsInCart: [],
        cardItemCount: 0,
        authToken: ""
    }

    componentDidUpdate(prevProps: Readonly<any>, prevState: Readonly<ShopAppState>, snapshot?: any) {
        localStorage.setItem("shopState", JSON.stringify(this.state))
    }

    onItemAddedToList(productNumber: number){
        if (this.state.productsInCart)
        this.setState({
            cardItemCount: this.state.cardItemCount + 1,
        })
    }

    render(){
        console.log(this.state);
          return(
            <div className="App">
                <Header />
                <main>
                    <button onClick={() => this.onItemAddedToList(1)}> Add random item</button>
                  <Router>
                      <Routes>
                          <Route path={"/"} element={<Shop />} />
                          <Route path={"/lol"} >
                            <Route path={":userId"} element={<SecondComponent />} />
                          </Route>
                      </Routes>
                  </Router>
                    <ControlledPopup />
                </main>
                <Footer />
            </div>
        )
  }
}

