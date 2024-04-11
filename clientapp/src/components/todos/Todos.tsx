import {Todo} from "../../App";
import TodoItem from "./TodoItem";

const Todos = ({
                   todos,
                   toggleDone,
                   handleDelete,
               }: {
    todos: Todo[];
    toggleDone: (id: string, done: boolean) => void;
    handleDelete: (id: string) => void;
}) => {
    return (
        <table className="flex flex-col gap-2">
            {todos.length ? (
                todos.map((t) => (
                    <TodoItem
                        key={t.id}
                        name={t.name}
                        deadline={t.deadline}
                        expired={t.expired}
                        done={t.done}
                        id={t.id}
                        toggleDone={toggleDone}
                        handleDelete={handleDelete}
                    />
                ))
            ) : (
                <span className="text-blue-100">No Todo yet!</span>
            )}
        </table>
    );
};

export default Todos;
