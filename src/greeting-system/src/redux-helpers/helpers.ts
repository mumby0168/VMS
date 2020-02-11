export const makeAction = <T, P>(type: T) => (payload: P) => {
    return {
        type,
        payload
    }
}

interface IStringMap<T> {
    [key: string]: T
}

type IAnyFunction = (...args: any[]) => any;

export type IActionUnion<A extends IStringMap<IAnyFunction>> = ReturnType<A[keyof A]>;