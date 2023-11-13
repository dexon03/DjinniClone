import React from "react";
import {
    AppBar,
    Avatar,
    Box,
    Button,
    Container, createTheme,
    IconButton,
    Menu,
    MenuItem, ThemeProvider,
    Toolbar,
    Tooltip,
    Typography
} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import AdbIcon from '@mui/icons-material/Adb';
import 'bootstrap/dist/css/bootstrap.css';
import { NavRoute } from "../models/nav.route.ts";
import { NavLink } from "react-router-dom";

export function HeaderComponent() {
    const pages: NavRoute[] = [
        {
            name: 'Offers',
            route: '/offers'
        },
        {
            name: 'Vacancies',
            route: '/vacancy',
        },
        {
            name: 'Salaries',
            route: '/salaries',
        }
    ]
    const settings = ['Profile', 'Logout'];
    const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

    let theme = createTheme({});
    theme = createTheme(theme, {
        palette: {
            myTheme: theme.palette.augmentColor({
                color: {
                    main: '#1a1a1a',
                    contrastText: '#ced4da',
                },
                name: 'myTheme',
            }),
        }
    })

    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };


    return (
        <ThemeProvider theme={theme}>
            <AppBar position="static" color="myTheme">
                <Container maxWidth="lg">
                    <Toolbar disableGutters>
                        <Typography
                            // variant="h6"
                            noWrap
                            component="a"
                            href="#app-bar-with-responsive-menu"
                            sx={{
                                mr: 2,
                                display: { xs: 'none', md: 'flex' },
                                fontFamily: 'Roboto,sans-serif',
                                fontWeight: 800,
                                letterSpacing: '.3rem',
                                color: 'inherit',
                                textDecoration: 'none',
                            }}
                        >
                            JobSearchApp
                        </Typography>

                        <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
                            <IconButton
                                size="large"
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleOpenNavMenu}
                                color="inherit"
                            >
                                <MenuIcon />
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorElNav}
                                anchorOrigin={{
                                    vertical: 'bottom',
                                    horizontal: 'left',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'left',
                                }}
                                open={Boolean(anchorElNav)}
                                onClose={handleCloseNavMenu}
                                sx={{
                                    display: { xs: 'block', md: 'none' },
                                }}
                            >
                                {pages.map((page) => (
                                    <MenuItem key={page.name} onClick={handleCloseNavMenu}>
                                        <Typography textAlign="center">{page.name}</Typography>
                                    </MenuItem>
                                ))}
                            </Menu>
                        </Box>
                        <AdbIcon sx={{ display: { xs: 'flex', md: 'none' }, mr: 1 }} />
                        <Typography
                            variant="h5"
                            noWrap
                            component="a"
                            sx={{
                                mr: 2,
                                display: { xs: 'flex', md: 'none' },
                                flexGrow: 1,
                                fontFamily: 'monospace',
                                fontWeight: 700,
                                letterSpacing: '.3rem',
                                color: 'inherit',
                                textDecoration: 'none',
                            }}
                        >
                            JobSearchApp
                        </Typography>
                        <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                            {pages.map((page) => (
                                <NavLink key={page.name} to={page.route} className="nav-link">
                                    <Typography
                                        variant="h6"
                                        noWrap
                                        component="a"
                                        sx={{
                                            mr: 2,
                                            display: { xs: 'none', md: 'flex' },
                                            flexGrow: 1,
                                            fontFamily: 'Open sans',
                                            color: 'inherit',
                                            textDecoration: 'none',
                                        }}
                                    >
                                        {page.name}
                                    </Typography>
                                </NavLink>
                            ))}
                        </Box>


                        <Box sx={{ flexGrow: 0 }}>
                            <Button variant="contained" color="success" className="m-3 rounded-5">online</Button>
                            <Tooltip title="Open settings">
                                <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                                    <Avatar alt="Remy Sharp" />
                                </IconButton>
                            </Tooltip>
                            <Typography
                                variant="h5"
                                noWrap
                                className="d-inline m-3 fs-4"
                                component="p"
                                sx={{
                                    fontFamily: 'Open sans',
                                }}
                            >

                                Name Surname
                            </Typography>
                            <Menu
                                sx={{ mt: '45px' }}
                                id="menu-appbar"
                                anchorEl={anchorElUser}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={Boolean(anchorElUser)}
                                onClose={handleCloseUserMenu}
                            >
                                {settings.map((setting) => (
                                    <NavLink to={setting} className={'nav-link'}>
                                        <MenuItem key={setting} onClick={handleCloseUserMenu}>
                                            <Typography textAlign="center">{setting}</Typography>
                                        </MenuItem>
                                    </NavLink>
                                ))}
                            </Menu>
                        </Box>
                    </Toolbar>
                </Container>
            </AppBar>
        </ThemeProvider>
    );
}