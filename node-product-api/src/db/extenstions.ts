import { BrandRepository } from "../brands/brand.repository";

interface IExtensions {
    brands: BrandRepository
}

export {
    IExtensions,
    BrandRepository
}