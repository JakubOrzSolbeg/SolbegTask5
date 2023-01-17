type Category = {
    categoryId: number,
    categoryName: string
}

type Brand = {
    brandId: number
    brandName: string
}

export default interface ShopInfo{
    categories: Array<Category>
    brands: Array<Brand>
}