import { SubmodelData } from "./submodel-data.model";

export interface Workbench {
    id: string;
    name: string;
    submodelData: SubmodelData[];
}