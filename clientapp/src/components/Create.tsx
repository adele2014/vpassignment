import { FormEvent, useState } from "react";

const CreateContainer = ({
                            handleSubmit,
                        }: {
    handleSubmit: (e: FormEvent<HTMLFormElement>, value: string) => void;
}) => {
    const [newTaskName, setNewTaskName] = useState("");
    return (
        <form
            action=""
            className="flex flex-col gap-4"
            onSubmit={(e) => {
                handleSubmit(e, newTaskName);
                setNewTaskName("");
            }}
        >
            <div className="flex flex-col">
                <label className="text-white">Enter your next todo:</label>
                <input
                    className="p-1 rounded-sm"
                    type="text"
                    placeholder="longer than 10 characters"
                    value={newTaskName}
                    onChange={(e) => setNewTaskName(e.target.value)}
                />
            </div>
            <div className="flex flex-col">
                <label className="text-white">Enter Description</label>
                <textarea
                    className="p-1 rounded-sm"
                    value={newTaskName}
                    rows="4"
                    onChange={(e) => setNewTaskName(e.target.value)}
                />
            </div>
            <div className="flex flex-col">
                <label className="text-white">Deadline</label>
                <input
                    className="p-1 rounded-sm"
                    type="date"
                    value={newTaskName}
                    onChange={(e) => setNewTaskName(e.target.value)}
                />
            </div>
            <button
                type="submit"
                className="bg-green-100 rounded-lg hover:bg-green-200 p-1"
            >
                Add a Todo
            </button>
        </form>
    );
};

export default CreateContainer;
