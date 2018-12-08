using Shield.Framework.Platform.Environment;
using SysEnv = System.Environment;

namespace Shield.Framework.Platform.Environement
{
	public class WindowsOperatingSystemInformation : PlatformOperatingSystemInformation
	{
	    protected override void GetOsDetails()
	    {
	        base.GetOsDetails();

	        var ver = new OperatingSystemVersionDetection(m_version, m_id);
	        m_name = string.Format("{0} {1}", ver.Name, ver.Edition);
	        m_version = ver.Version;
	    }
	}
}