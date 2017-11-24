
// tslint:disable-next-line:class-name
export interface ISelf {
    href: string;
}

// tslint:disable-next-line:class-name
export interface ILinks {
    self: ISelf;
    brewery: Array<ISelf>;
}

export interface IBrewery {
    _links: ILinks;
}
