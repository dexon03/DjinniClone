import useToken from '../../hooks/useToken';
import { Role } from '../../models/common/role.enum';
import CandidateProfileComponent from '../../components/candidate.profile.component';
import RecruiterProfileComponent from '../../components/recruiter.profile.component';

const ProfilePage = () => {
  const { token, setToken } = useToken();

  const handleSubmit = (e) => {
    e.preventDefault();
    // Add your form submission logic here
  };

  return (
    token?.role == Role[Role.Candidate]
      ? <CandidateProfileComponent id={token.userId} />
      : <RecruiterProfileComponent id={token.userId} />
  );
};

export default ProfilePage;
