var solution = new Solution('Blocks');
var project = new Project('Blocks');
project.setDebugDir('build/linux');
project.addSubProject(Solution.createProject('build/linux-build'));
project.addSubProject(Solution.createProject('/usr/lib/haxe/lib/kha/16,1,2'));
project.addSubProject(Solution.createProject('/usr/lib/haxe/lib/kha/16,1,2/Kore'));
solution.addProject(project);
return solution;
