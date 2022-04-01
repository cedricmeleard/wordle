import Dummy from "./dummy";

test('dummy test', () => {
    const dummy = new Dummy()
    expect(dummy.sayHello("Michel")).toEqual("Hello Michel");
})