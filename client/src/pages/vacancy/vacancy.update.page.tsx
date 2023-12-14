import { useNavigate, useParams } from "react-router-dom";
import { useGetVacancyQuery, useLazyGetVacancyCategoriesQuery, useLazyGetVacancyLocationQuery, useLazyGetVacancySkillsQuery } from "../../app/features/vacancy/vacancy.api";
import useToken from "../../hooks/useToken";
import { useEffect, useState } from "react";
import { AttendanceMode } from "../../models/common/attendance.enum";
import { Experience } from "../../models/vacancy/experience.enum";

export function VacancyUpdatePage() {
    const { id } = useParams();
    const { data: vacancy, isLoading, isError, error } = useGetVacancyQuery(id);
    const [getVacancySkills, { data: skills, isError: isSkillsLoadingError }] = useLazyGetVacancySkillsQuery();
    const [getVacancyLocations, { data: locations, isError: isErrorLoadingError }] = useLazyGetVacancyLocationQuery();
    const [getVacancyCategories, { data: categories, isError: isCategoriesLoadingError }] = useLazyGetVacancyCategoriesQuery();
    const { token } = useToken();
    const navigate = useNavigate();

    const [title, setTitle] = useState('');
    const [positionTitle, setPositionTitle] = useState('');
    const [description, setDescription] = useState('');
    const [salary, setSalary] = useState(0);
    const [experience, setExperience] = useState<Experience>(0);
    const [attendanceMode, setAttendanceMode] = useState<AttendanceMode>(0);
    const [selectedCategory, setSelectedCategory] = useState('');
    const [selectedLocations, setSelectedLocations] = useState<string[]>([]);
    const [selectedSkills, setSelectedSkills] = useState<string[]>([]);


    useEffect(() => {
        if (vacancy) {
            setTitle(vacancy.title);
            setPositionTitle(vacancy.positionTitle);
            setDescription(vacancy.description);
            setSalary(vacancy.salary);
            setExperience(vacancy.experience);
            setAttendanceMode(vacancy.attendanceMode);
            setSelectedCategory(vacancy.category.id);
            setSelectedLocations(vacancy.locations.map(location => location.id));
            setSelectedSkills(vacancy.skills.map(skill => skill.id));
            getVacancyCategories();
            getVacancyLocations();
            getVacancySkills();
        }
    }, [vacancy])

    if (token?.role == 'Candidate') {
        return <p>Access denied</p>
    }

    if (isError || isSkillsLoadingError || isErrorLoadingError || isCategoriesLoadingError) {
        return <p>Error</p>
    }

    if (isLoading) {
        return <p>Loading...</p>
    }

    return (
        
    )


}