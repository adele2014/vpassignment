const TodoItem = ({
                      name,
                      deadline,
                      expired,
                      done,
                      id,
                      toggleDone,
                      handleDelete,
                  }: {
    name: string;
    done: boolean;
    id: string;
    deadline: string;
    expired: string;
    toggleDone: (id: string, done: boolean) => void;
    handleDelete: (id: string) => void;
}) => {
    return (
        <tr className="flex justify-between bg-white p-1 px-3 rounded-sm gap-4">
            <td>
            <div className="flex gap-2 items-center">
                <input
                    type="checkbox"
                    checked={done}
                    onChange={() => toggleDone(id, !done)}
                />
            </div>
        </td>
                <td>{name}</td>
            <td>{deadline}</td>
            <td>{expired}</td>
           <td>
            <button
                className="bg-green-200 hover:bg-green-300 rounded-lg p-1 px-3"
                type="button"
                onClick={() => handleDelete(id)}
            >
                Delete
            </button>
           </td>
        </tr>
    );
};

export default TodoItem;
