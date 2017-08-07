import { MyFirstAppPage } from './app.po';

describe('my-first-app App', () => {
  let page: MyFirstAppPage;

  beforeEach(() => {
    page = new MyFirstAppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
