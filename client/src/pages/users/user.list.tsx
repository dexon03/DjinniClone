import { TableContainer, Table, TableHead, TableRow, TableCell, TableBody, Button } from "@mui/material";
import { useDeleteUserMutation, useGetUsersQuery } from "../../app/features/users/usersApi";
import { useNavigate } from "react-router-dom";

export function UserList() {
    const { data: users, isLoading, refetch } = useGetUsersQuery();
    const [deleteUser, { isLoading: isDeleting }] = useDeleteUserMutation();
    const navigate = useNavigate();

    const handleEdit = (userId) => {
        navigate(`/users/${userId}`);
    };

    const handleDelete = (userId) => {
        deleteUser(userId);
        refetch();
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>First Name</TableCell>
                        <TableCell>Last Name</TableCell>
                        <TableCell>Email</TableCell>
                        <TableCell>Role Name</TableCell>
                        <TableCell align="center">Action</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {users && users.map((user) => (
                        <TableRow key={user.id}>
                            <TableCell>{user.firstName}</TableCell>
                            <TableCell>{user.lastName}</TableCell>
                            <TableCell>{user.email}</TableCell>
                            <TableCell>{user.role.name}</TableCell>
                            <TableCell align="center">
                                <Button variant="contained" color="primary" onClick={() => handleEdit(user.id)}>
                                    Edit
                                </Button>{' '}
                                <Button variant="contained" color="error" onClick={() => handleDelete(user.id)}>
                                    Delete
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};


