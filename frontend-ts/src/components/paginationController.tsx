import React from "react";
import exp from "constants";

type PaginationProps = {
    subclass: string
    currentPage: number
    changePage: (page: number) => void
    changeItemPerPage: (items: number) => void
    maxPage: number
}


type PaginationState = {
    currentPage: number,
    itemsPerPage: number,
    maxPage: number
}

export class PaginationController extends React.Component<PaginationProps, PaginationState>{
    constructor(props: PaginationProps) {
        super(props);
    }

    state: PaginationState = {
        currentPage: 1,
        itemsPerPage: 5,
        maxPage: 10
    }


    render() {
        return (
            <div className={"pagination-controll " + this.props.subclass}>
                <label>

                </label>
                <select onChange={(e) => this.props.changeItemPerPage(Number(e.target.value))} defaultValue={5}>
                    <option value={1} > 1 </option>
                    <option value={5} > 5 </option>
                    <option value={10} > 10 </option>
                    <option value={20} > 20 </option>
                </select>
                <button disabled={this.props.currentPage <= 1} onClick={() => this.props.changePage(1)}>
                    &lt;&lt;
                </button>
                <button disabled={this.props.currentPage <= 1} onClick={() => this.props.changePage(this.props.currentPage - 1)}>
                    &lt;
                </button>
                <button>
                    {this.props.currentPage}
                </button>
                <button disabled={this.props.currentPage >= this.props.maxPage} onClick={() => this.props.changePage(this.props.currentPage + 1)}>
                    &gt;
                </button>
                <button disabled={this.props.currentPage >= this.props.maxPage} onClick={() => this.props.changePage(this.props.maxPage)}>
                    &gt;&gt;
                </button>
            </div>
        )
    }

}