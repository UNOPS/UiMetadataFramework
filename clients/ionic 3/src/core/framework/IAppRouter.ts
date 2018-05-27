export interface IAppRouter {
	go: (form: string, values) => void;
	makeUrl: (form: string, values) => string;
}