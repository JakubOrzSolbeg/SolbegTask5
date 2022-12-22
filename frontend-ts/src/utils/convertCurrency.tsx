
export default function ConvertCurrency(value: number): string{
    return value.toLocaleString('pl-PL', {
        minimumIntegerDigits: 1,
        useGrouping: false,
        maximumFractionDigits: 2,
        minimumFractionDigits: 2,
        currency: "pln"
    })
}