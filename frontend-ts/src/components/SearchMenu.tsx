import React from "react";

type SearchMenuState = {
    minValue: number
    maxValue: number
    brands: Array<number>
    categories: Array<number>
    name: string | null
}


export class SearchManu extends React.Component<any, SearchMenuState>{
    constructor(props: any) {
        super(props);

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    state: SearchMenuState = {
        minValue: 0,
        maxValue: 30000,
        brands: [],
        categories: [],
        name: null
    }

    handleChange(event: Event){
        console.log("Form state changed")
    }

    handleSubmit(event: Event){
        console.log(this.state)
        event.preventDefault()
    }


    render() {
        return(
            <div className={"search-box"}>
                <form className={"search-form"}>
                    <label>
                        Min price:
                        <input type={"number"}/>
                    </label>
                    <label>
                        Max price:
                        <input type={"number"} />
                    </label>
                    <label>
                        Category:
                        <input type={"checkbox"}/>
                    </label>
                    <label>
                        Name:
                        <input type={"text"}/>
                    </label>
                    <input/>

                </form>
            </div>
        )
    }

}
