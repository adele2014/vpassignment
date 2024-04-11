import { Todo } from "../../App";
import SummaryItem from "./SummaryItem"

const Summary = ({ tasks }: { tasks: Todo[] }) => {
    const total = tasks.length;
    const pending = tasks.filter((t) => !t.done).length;
    const done = tasks.filter((t) => t.done).length;
    return (
        <>
            <div className="flex flex-col gap-1 sm:flex-row sm:justify-between">
                <SummaryItem itemName={"Total"} itemValue={total} />
                <SummaryItem itemName={"In Progress"} itemValue={pending} />
                <SummaryItem itemName={"Done"} itemValue={done} />
            </div>
        </>
    );
};

export default Summary;
